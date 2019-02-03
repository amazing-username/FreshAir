package com.example.freshair.database

import android.content.ContentValues
import android.content.Context
import android.database.Cursor
import android.database.sqlite.SQLiteConstraintException
import android.database.sqlite.SQLiteDatabase
import android.database.sqlite.SQLiteException
import android.database.sqlite.SQLiteOpenHelper

import com.example.freshair.models.User as Um

import java.util.ArrayList

class UsersDBHelper(context: Context) : SQLiteOpenHelper(context, DATABASE_NAME, null, DATABASE_VERSION) {
    override fun onCreate(db: SQLiteDatabase) {
        db.execSQL(SQL_CREATE_ENTRIES)
    }

    override fun onUpgrade(db: SQLiteDatabase, oldVersion: Int, newVersion: Int) {
        db.execSQL(SQL_DELETE_ENTRIES)
        onCreate(db)
    }

    override fun onDowngrade(db: SQLiteDatabase, oldVersion: Int, newVersion: Int)
    {
        onUpgrade(db, oldVersion, newVersion)
    }

    @Throws(SQLiteConstraintException::class)
    fun insertUser(user: com.example.freshair.models.User): Boolean {
        val db = writableDatabase

        val values = ContentValues()
        values.put(DBContract.UserEntry.COLUMN_ID, user.id)
        values.put(DBContract.UserEntry.COLUMN_FIRSTNAME, user.firstname)
        values.put(DBContract.UserEntry.COLUMN_LASTNAME, user.lastname)
        values.put(DBContract.UserEntry.COLUMN_EMAIL, user.email)
        values.put(DBContract.UserEntry.COLUMN_USERNAME, user.username)
        values.put(DBContract.UserEntry.COLUMN_PASSWORD, user.password)

        val newRowId = db.insert(DBContract.UserEntry.TABLE_NAME, null, values)

        return true
    }

    @Throws(SQLiteConstraintException::class)
    fun deleteUser(id: String): Boolean {
       val db = writableDatabase

        val selection = DBContract.UserEntry.COLUMN_ID + " LIKE ?"

        val selectionArgs = arrayOf(id)

        db.delete(DBContract.UserEntry.TABLE_NAME, selection, selectionArgs)

        return true
    }

    @Throws(SQLiteConstraintException::class)
    fun readUser(id: String): ArrayList<Um> {
        val users = ArrayList<Um>()
        val db = writableDatabase
        var cursor: Cursor? = null
        try {
            cursor = db.rawQuery("select * from " + DBContract.UserEntry.TABLE_NAME + " WHERE " +
                    DBContract.UserEntry.COLUMN_ID + "='" + id + "'", null )
        }
        catch (e : SQLiteException) {
            db.execSQL(SQL_CREATE_ENTRIES)
            return ArrayList()
        }

        var firstname: String
        var lastname: String
        var username: String
        var password: String
        var email: String

        if (cursor!!.moveToFirst()) {
            while (!cursor.isAfterLast ) {
                firstname = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_FIRSTNAME))
                lastname = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_LASTNAME))
                email = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_EMAIL))
                username = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_USERNAME))
                password = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_PASSWORD))

                users.add(Um(id.toInt(), firstname, lastname, email, username, password))
                cursor.moveToNext()
            }
        }
        return users
    }

    fun readAllUsers(): ArrayList<Um> {
        val users = ArrayList<Um>()
        val db = writableDatabase
        var cursor: Cursor? = null
        try {
            cursor = db.rawQuery("select * from " + DBContract.UserEntry.TABLE_NAME, null)
        }
        catch (e: SQLiteException) {
            db.execSQL(SQL_CREATE_ENTRIES)
            return ArrayList()
        }

        var id: Int
        var firstname: String
        var lastname: String
        var email: String
        var username: String
        var password: String
        if (cursor!!.moveToFirst()) {
            while (!cursor.isAfterLast) {
                id = cursor.getInt(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_ID))
                firstname = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_USERNAME))
                lastname = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_LASTNAME))
                email = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_EMAIL))
                username = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_USERNAME))
                password = cursor.getString(cursor.getColumnIndex(DBContract.UserEntry.COLUMN_PASSWORD))

                users.add(Um(id, firstname, lastname, email, username, password))
                cursor.moveToNext()
            }
        }

        return users
    }


    companion object {
        val DATABASE_VERSION = 1
        val DATABASE_NAME = "FreshAir.db"

        private val SQL_CREATE_ENTRIES = "CREATE TABLE " +
                DBContract.UserEntry.TABLE_NAME + " (" +
                DBContract.UserEntry.COLUMN_ID + " INT PRIMARY KEY," +
                DBContract.UserEntry.COLUMN_FIRSTNAME + " TEXT," +
                DBContract.UserEntry.COLUMN_LASTNAME + " TEXT," +
                DBContract.UserEntry.COLUMN_EMAIL + " TEXT," +
                DBContract.UserEntry.COLUMN_USERNAME + " TEXT," +
                DBContract.UserEntry.COLUMN_PASSWORD + " TEXT)"

        private val SQL_DELETE_ENTRIES = "DROP TABLE IF EXISTS " + DBContract.UserEntry.TABLE_NAME
    }
}