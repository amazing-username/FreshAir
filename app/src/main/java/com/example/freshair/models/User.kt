package com.example.freshair.models

class User {

    constructor (id: Int, firstname: String, lastname: String, email: String,
                 username: String, password: String)
    {
        this.id = id
        this.firstname = firstname
        this.lastname = lastname
        this.email = email
        this.username = username
        this.password = password
    }
    constructor (firstname: String, lastname: String, email: String,
                 username: String, password: String)
    {
        this.firstname = firstname
        this.lastname = lastname
        this.email = email
        this.username = username
        this.password = password
    }

    var id: Int = 1
    var firstname : String = ""
    var lastname : String = ""
    var email : String = ""
    var username : String = ""
    var password : String = ""
}