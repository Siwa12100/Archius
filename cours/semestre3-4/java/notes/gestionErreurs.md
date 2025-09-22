# Gestion des erreurs en java

### throws
Il est possible de mettre dans le prototype des méthodes un `throws typeException` pour indiquer que la méthode pourrait renvoyer un certains type d'exceptions. En fait, cela permet d'obliger la personne qui utilisera la méthode à la protéger dans un bloc try catch, sinon cela ne compilera pas. 

Par exemple, dans le cadre de la serialisation, si je n'entoure pas d'un bloc try catch ma methode, cela me met une erreur : 
```java
public void serialiserVehicule(Vehicule v) {
        try {
            this.serialiser.writeObject(v);
        } catch (IOException e) {
            e.printStackTrace();
        }
```

C'est parce que dans le prototype de la fonction writeObject, il y a un throws, dans le genre : 
```java
.... writeObject(parametres...) throws IOException {
    ....
}
```