package com.lg;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.Query;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        System.out.println("JPA project");
        EntityManagerFactory factory = Persistence.createEntityManagerFactory("Hibernate_JPA");
        EntityManager em = factory.createEntityManager();
        User[] users = new User[5];
        users[0] = new User(null, "test_1","test_1","Andrzej","Kowalski", Sex.MALE);
        users[1] = new User(null, "awanna","test_2","Anna","Wanna", Sex.FEMALE);
        users[2] = new User(null, "kszczecin","test_3","Katarzyna","Zeszczecina", Sex.FEMALE);
        users[3] = new User(null, "hfelga","test_4","Helga","Felga", Sex.FEMALE);
        users[4] = new User(null, "tdomek","test_5","Tomek","Domek", Sex.MALE);
        Role[] roles = new Role[5];
        roles[0] = new Role(null, "User");
        roles[1] = new Role(null, "Helper");
        roles[2] = new Role(null, "Moderator");
        roles[3] = new Role(null, "Admin");
        roles[4] = new Role(null, "Owner");
        // Dodaj użytkowników
        em.getTransaction().begin();
        for (int i = 0; i < 5; i++) em.persist(users[i]);
        em.getTransaction().commit();
        // Dodaj role
        em.getTransaction().begin();
        for (int i = 0; i < 5; i++) em.persist(roles[i]);
        em.getTransaction().commit();
        // Wyszukanie roli o id = 5
        em.getTransaction().begin();
        Query q1 = em.createQuery("SELECT r FROM Role r WHERE r.id = 5");
        List<Role> r5 = q1.getResultList();
        System.out.println("Rola o id = 5: " + r5.get(0).getName());
        em.getTransaction().commit();
        // Usunięcie roli o id = 5
        em.getTransaction().begin();
        Query q2 = em.createQuery("DELETE FROM Role r WHERE r.id = 5");
        em.getTransaction().commit();
        // Wyszukanie wszystkich kobiet w bazie danych
        em.getTransaction().begin();
        Query q3 = em.createQuery("SELECT u FROM User u WHERE u.sex = 'FEMALE'");
        List<User> kobiety = q3.getResultList();
        System.out.println("Wszystkie kobiety w bazie danych:");
        for (int i = 0; i < kobiety.size(); i++) System.out.println(kobiety.get(i).getFirstName() + " "
                + kobiety.get(i).getLastName() + " ; " + kobiety.get(i).getLogin());
        em.getTransaction().commit();
        // Dodanie użytkownika z 2 rolami
        em.getTransaction().begin();
        User u6 = new User(null, "mobiektowy","haslo","Maciej","Obiektowy", Sex.MALE);
        u6.addRole(roles[3]);
        u6.addRole(roles[0]);
        em.merge(u6);
        em.getTransaction().commit();
        // Zadanie 4.4.5
        em.getTransaction().begin();
        UsersGroup[] groups = new UsersGroup[2];
        groups[0] = new UsersGroup();
        groups[1] = new UsersGroup();
        for (int i = 0; i < 2; i++) em.persist(groups[i]);
        em.getTransaction().commit();
        em.getTransaction().begin();
        users[0].addToGroup(groups[0]);
        users[1].addToGroup(groups[0]);
        users[1].addToGroup(groups[1]);
        users[2].addToGroup(groups[1]);
        users[3].addToGroup(groups[0]);
        users[4].addToGroup(groups[1]);
        for (int i = 0; i < 5; i++) em.merge(users[i]);
        em.getTransaction().commit();
        em.close();
        factory.close();
    }
}
