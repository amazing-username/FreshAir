package com.example.freshair.management

import android.content.Context
import android.provider.SyncStateContract.Helpers.insert
import android.database.sqlite.SQLiteOpenHelper
import android.database.sqlite.SQLiteDatabase
import android.provider.SyncStateContract.Helpers.get
import org.jetbrains.anko.db.*

import com.example.freshair.management.LoginManager.User as UserDB

class DatabaseManager {

    fun saveCredentials(user: UserDB) {
        /**
        val Cdatabase: MyDatabaseOpenHelper
        get() = MyDatabaseOpenHelper.getInstance(this@Lo)
        var database: MyDatabaseOpenHelper(this)
        database.use {
            insert("User",
                "id" to user.id,
                "username" to user.username,
                "accessToken" to user.accessToken,
                "firstLogin" to user.isFirstLogin)
        }
        */
    }

    class MyDatabaseOpenHelper(ctx: Context) : ManagedSQLiteOpenHelper(ctx, "FreshAir", null, 1) {
        companion object {
            private var instance: MyDatabaseOpenHelper? = null

            @Synchronized
            fun getInstance(ctx: Context): MyDatabaseOpenHelper {
                if (instance == null) {
                    instance = MyDatabaseOpenHelper(ctx.getApplicationContext())
                }
                return instance!!
            }
        }

        override fun onCreate(db: SQLiteDatabase) {
            // Here you create tables
            db.createTable("User", true,
                "id" to INTEGER + PRIMARY_KEY + UNIQUE,
                "name" to TEXT,
                "photo" to BLOB)
        }

        override fun onUpgrade(db: SQLiteDatabase, oldVersion: Int, newVersion: Int) {
            // Here you can upgrade tables, as usual
            db.dropTable("User", true)
        }
    }

    // Access property for Context
    val Context.database: MyDatabaseOpenHelper
        get() = MyDatabaseOpenHelper.getInstance(getApplicationContext())
}