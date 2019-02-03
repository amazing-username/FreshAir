package com.example.freshair.database

import android.provider.BaseColumns

object DBContract {
    class UserEntry : BaseColumns {
        companion object {
            val TABLE_NAME = "users"
            val COLUMN_ID = "id"
            val COLUMN_EMAIL = "email"
            val COLUMN_USERNAME = "username"
            val COLUMN_PASSWORD = "password"
            val COLUMN_FIRSTNAME = "firstname"
            val COLUMN_LASTNAME = "lastname"
        }
    }
}