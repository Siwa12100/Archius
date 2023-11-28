package fr.toxio.elendil.listener;

import fr.toxio.elendil.Elendil;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.Location;
import org.bukkit.Material;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Horse;
import org.bukkit.entity.Player;
import org.bukkit.entity.Vehicle;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.vehicle.VehicleEnterEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;
import org.bukkit.scheduler.BukkitRunnable;
import org.bukkit.util.Vector;

import java.util.HashMap;
import java.util.Map;

public class MainListener implements Listener {

    private int horseMovementTaskID;
    private int horseDespawnTaskID;
    private Map<Player, Boolean> playerOnHorse = new HashMap<>();
    private Map<Player, Horse> playerHorse = new HashMap<>();

    @EventHandler
    public void onInteract(PlayerInteractEvent event) {
        Player player = event.getPlayer();
        if (event.getItem() == null) {
            return;
        }
        
        if (event.getItem() != null && event.getItem().getType() == Material.SADDLE) {
            if (event.getItem().hasItemMeta() && event.getItem().getItemMeta().hasDisplayName() && event.getItem().getItemMeta().getDisplayName().equals(ChatColor.GOLD + "Cheval")) {
                if (playerHorse.containsKey(player)) {
                    player.sendMessage(ChatColor.RED + "Vous avez deja un cheval");
                    return;
                }
                Location horseLocation = player.getLocation().clone().add(player.getLocation().getDirection().multiply(-10));

                Horse horse = (Horse) player.getWorld().spawnEntity(horseLocation.add(2,0,0), EntityType.HORSE);
                horse.setTamed(true);
                horse.addPotionEffect(new PotionEffect(PotionEffectType.SPEED, Integer.MAX_VALUE,1,false,false));
                horse.getInventory().setSaddle(new ItemStack(Material.SADDLE));
                playerHorse.put(player, horse);
                System.out.println(horse);
                player.sendMessage("Vous venez de siffler votre cheval, il va bientôt arriver");
                horseMovementTaskID = Bukkit.getScheduler().runTaskTimer(Elendil.getInstance(), () -> {
                    if (!horse.isDead()) {
                        double distance = horse.getLocation().distance(player.getLocation());
                        if (distance < 2.0) {
                            Bukkit.getScheduler().cancelTask(horseMovementTaskID);
                        } else {
                            Vector direction = player.getLocation().toVector().subtract(horse.getLocation().toVector()).normalize();
                            horse.setVelocity(direction);
                        }
                    }
                }, 0L, 15L).getTaskId();
                horseDespawnTaskID = Bukkit.getScheduler().runTaskTimer(Elendil.getInstance(), () -> {
                    if (horse.isEmpty()) {
                        horse.remove();
                        playerHorse.remove(player);
                        player.sendMessage("Votre cheval est parti car vous ne l'avez pas utilisé durant 40 secondes");
                        Bukkit.getScheduler().cancelTask(horseDespawnTaskID);
                    }
                }, 20L * 40L, 20L * 40L).getTaskId();
            }

        }
    }
    @EventHandler
    public void onMount(VehicleEnterEvent event) {
        if (event.getEntered() instanceof Player && event.getVehicle() instanceof Horse) {
            Player player = (Player) event.getEntered();
            playerOnHorse.put(player, true);
            Bukkit.getScheduler().cancelTask(horseDespawnTaskID);
        }
    }

    @EventHandler
    public void onDismount(org.bukkit.event.vehicle.VehicleExitEvent event) {
        if (event.getVehicle() instanceof Horse) {
            Player player = (Player) event.getExited();
            playerOnHorse.put(player, false);
            player.sendMessage("Vous venez de descendre de votre cheval");
            horseDespawnTaskID = Bukkit.getScheduler().runTaskTimer(Elendil.getInstance(), () -> {
                Horse horse = playerHorse.get(player);
                if (horse!= null && horse.isEmpty()) {
                    horse.remove();
                    playerHorse.remove(player);
                    player.sendMessage("Votre cheval est parti car vous ne l'avez pas utilisé durant 40 secondes");
                    Bukkit.getScheduler().cancelTask(horseDespawnTaskID);
                }
            }, 20L * 40L, 20L * 40L).getTaskId();
        }
    }
    @EventHandler
    public void onJoin(PlayerJoinEvent event) {
        Player player = event.getPlayer();
        player.getInventory().addItem(getItem(Material.SADDLE, ChatColor.GOLD + "Cheval"));
    }
    public ItemStack getItem(Material material, String customName) {
        ItemStack it = new ItemStack(material, 1);
        ItemMeta itM = it.getItemMeta();
        if (customName != null) itM.setDisplayName(customName);
        it.setItemMeta(itM);
        return it;
    }
}

