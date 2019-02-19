package com.example.freshair.management

import java.lang.Exception

import org.jetbrains.anko.*
import org.json.JSONObject
import khttp.responses.Response

import com.example.freshair.models.User

import khttp.delete as httpDelete

class RegisterManager {
    constructor(user: User) {
        this.user = user
    }

    fun login() {

    }
    fun registerUser() {
        doAsync {
            sendRegisterRequest()
        }
    }


    private fun sendRegisterRequest() {
        var url = this.url.plus("register/user/")
        try {
            var response =
                khttp.post(
                    url = url,
                    json = mapOf(
                        "first_name" to user.firstname, "last_name" to user.lastname, "email" to user.email,
                        "username" to user.username, "password" to user.password
                    )
                )
            val obj: JSONObject = response.jsonObject

        }
        catch (s: Exception) {
            print(s.stackTrace)
            print("\n\n${s.message}")
        }
        finally {
            print("sd")
        }
    }

    var user = User("","","","","")
    val url = "http://50.88.81.55:8024/api/"
}