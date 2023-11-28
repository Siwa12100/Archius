package fr.toxio.elendil;

import fr.toxio.elendil.commands.GiveCommands;
import fr.toxio.elendil.listener.MainListener;
import org.bukkit.Bukkit;
import org.bukkit.plugin.java.JavaPlugin;

public final class Elendil extends JavaPlugin {

    public static Elendil instance;

    @Override
    public void onEnable() {
        instance = this;
        Bukkit.getServer().getPluginManager().registerEvents(new MainListener(), this);
        Bukkit.getServer().getPluginCommand("givehorse").setExecutor(new GiveCommands());
    }

    @Override
    public void onDisable() {

    }

    public static Elendil getInstance() {
        return instance;
    }
}
