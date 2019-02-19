package com.example.freshair.management

import java.lang.Exception

import org.jetbrains.anko.*
import org.json.JSONObject
import khttp.responses.Response

import com.example.freshair.models.User

class LoginManager {
    constructor(user: User) {
        this.user = user
    }



    fun login() {
        doAsync {
            sendLoginRequest()
        }
    }

    private fun sendLoginRequest() {
        var url = this.url.plus("login/")
        try {
            var response =
                khttp.post(
                    url = url,
                    json = mapOf(
                        "username" to user?.username, "password" to user?.password
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


    val user: User?
    val url = "http://50.88.81.55:8024/api/"
}