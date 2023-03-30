package com.lg;

import javax.persistence.*;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;

@Entity
public class UsersGroup {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToMany(mappedBy = "groups")
    private final Set<User> users = new HashSet<>();

    public UsersGroup() {}

    public void addUser(User u) {
        this.users.add(u);
    }

    public Set<User> getUsers() {
        return users;
    }
}
