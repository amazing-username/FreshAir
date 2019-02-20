package com.example.freshair.management

import java.lang.Exception

import org.jetbrains.anko.*
import org.json.JSONObject
import khttp.responses.Response
import kotlinx.android.synthetic.main.activity_login.view.*

import com.example.freshair.models.User as UserAuth

class LoginManager {
    constructor(user: UserAuth) {
        this.user = user
    }



    fun login() {

        doAsync {
            loginSuccessful =  sendLoginRequest()
        }.get()

        if (!loginSuccessful) {
            return
        }
        userSuccessful = parseLoginResults()
    }


    private fun parseLoginResults(): User  {
        val accessToken = obj.get("token").toString()
        val username = obj.get("username").toString()
        val id = obj.get("id").toString().toInt()
        val firstLogin = obj.get("first_login").toString().toBoolean()

        val user = User(id, accessToken, username, user!!.password, firstLogin)

        return user
    }

    private fun sendLoginRequest(): Boolean {
        var url = this.url.plus("login/")
        try {
            var response =
                khttp.post(
                    url = url,
                    json = mapOf(
                        "username" to user?.username, "password" to user?.password
                    )
                )
            obj = response.jsonObject

        }
        catch (s: Exception) {
            return false
        }
        finally {
            return true
        }
        return true
    }


    val user: UserAuth?
    val url = "http://50.88.81.55:8024/api/"
    var obj = JSONObject()
    var userSuccessful: User? = null
    var loginSuccessful = false

    class User(var id: Int, var accessToken: String, var username: String, var password: String,
               var isFirstLogin: Boolean) {
    }
}