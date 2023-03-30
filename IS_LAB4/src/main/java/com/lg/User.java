package com.lg;

import javax.persistence.*;
import java.util.*;

@Entity
@Table(name="users", indexes = @Index(name="idx_login", columnList = "login"))
public class User {
    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    private Long id;
    private String firstName;
    private String lastName;
    @Column(nullable = false, unique = true)
    private String login;
    @Column(nullable = false)
    private String password;

    @Enumerated(EnumType.STRING)
    private Sex sex;

    @OneToMany(fetch = FetchType.EAGER, cascade = CascadeType.ALL)
    private final List<Role> roles = new ArrayList<>();

    @ManyToMany(fetch = FetchType.EAGER, cascade = CascadeType.ALL)
    private final Set<UsersGroup> groups = new HashSet<>();

    public User() {

    }

    public User(Long id, String login, String password, String firstName, String lastName, Sex sex) {
        this.id = id;
        this.login = login;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.sex = sex;
    }

    public void addRole(Role r) {
        this.roles.add(r);
    }

    public void addToGroup(UsersGroup g) {
        this.groups.add(g);
        g.getUsers().add(this);
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getLogin() {
        return login;
    }

    public void setLogin(String login) {
        this.login = login;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public Sex getSex() {
        return sex;
    }

    public void setSex(Sex sex) {
        this.sex = sex;
    }
}
