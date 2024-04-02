package VeraxFeather.authentification;

public class AuthenticationRequest {

    private String username;
    private String password;

    public AuthenticationRequest(String nomUser, String mdp) {
        this.username = nomUser;
        this.password = mdp;
    }

    // Getters et setters
    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
