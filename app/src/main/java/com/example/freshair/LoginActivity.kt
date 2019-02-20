package com.example.freshair

import android.animation.Animator
import android.animation.AnimatorListenerAdapter
import android.annotation.TargetApi
import android.support.v7.app.AppCompatActivity
import android.app.LoaderManager.LoaderCallbacks
import android.content.Context
import android.content.CursorLoader
import android.content.Loader
import android.database.Cursor
import android.net.Uri
import android.os.AsyncTask
import android.os.Build
import android.os.Bundle
import android.provider.ContactsContract
import android.view.View
import android.view.inputmethod.EditorInfo
import android.widget.TextView

import java.util.ArrayList
import android.content.Intent
import android.database.sqlite.SQLiteDatabase
import android.provider.SyncStateContract
import com.example.freshair.management.LoginManager

import kotlinx.android.synthetic.main.activity_login.*

import com.example.freshair.models.User
import org.jetbrains.anko.db.*


/**
 * A login screen that offers login via email/password.
 */
class LoginActivity : AppCompatActivity(), LoaderCallbacks<Cursor> {
    /**:179@author
     * Keep track of the login task to ensure we can cancel it if requested.
     */

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)
        // Set up the login form.
        populateCredentials()
        password.setOnEditorActionListener(TextView.OnEditorActionListener { _, id, _ ->
            if (id == EditorInfo.IME_ACTION_DONE || id == EditorInfo.IME_NULL) {
                //functionName()()
                return@OnEditorActionListener true
            }
            false
        })

        login.setOnClickListener() { loginUser() }
        register.setOnClickListener { registerUser() }
    }




    /**
     * Attempts to sign in or register the account specified by the login form.
     * If there are form errors (invalid email, missing fields, etc.), the
     * errors are presented and no actual login attempt is made.
     *
     */

    fun loginUser() {

        var usr = parseUser()

        val loginMgr = LoginManager(usr)
        loginMgr.login()

        var userResult = loginMgr.userSuccessful

        var dbMgr = DatabaseManager()
        if (checkBox.isChecked) {
            dbMgr.saveCredentials(userResult!!, this)
        }


        val intent = Intent(this, MainActivity::class.java)

        startActivity(intent)
    }

    fun registerUser() {
        val intent = Intent(this, RegisterActivity::class.java)

        startActivity(intent)
    }


    private fun parseUser(): User {
        val usrName = username.text.toString()
        val passWord = password.text.toString()

        return User("", "","",usrName, passWord)
    }

    private fun populateCredentials() {
        var db = DatabaseManager()
        var usr = db.retrieveCredentials(this)
        username.setText(usr.username)
        password.setText(usr.password)
    }

    /**
     * Shows the progress UI and hides the login form.
     */
    @TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
    private fun showProgress(show: Boolean) {
        // On Honeycomb MR2 we have the ViewPropertyAnimator APIs, which allow
        // for very easy animations. If available, use these APIs to fade-in
        // the progress spinner.
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB_MR2) {
            val shortAnimTime = resources.getInteger(android.R.integer.config_shortAnimTime).toLong()

            login_form.visibility = if (show) View.GONE else View.VISIBLE
            login_form.animate()
                .setDuration(shortAnimTime)
                .alpha((if (show) 0 else 1).toFloat())
                .setListener(object : AnimatorListenerAdapter() {
                    override fun onAnimationEnd(animation: Animator) {
                        login_form.visibility = if (show) View.GONE else View.VISIBLE
                    }
                })

            login_progress.visibility = if (show) View.VISIBLE else View.GONE
            login_progress.animate()
                .setDuration(shortAnimTime)
                .alpha((if (show) 1 else 0).toFloat())
                .setListener(object : AnimatorListenerAdapter() {
                    override fun onAnimationEnd(animation: Animator) {
                        login_progress.visibility = if (show) View.VISIBLE else View.GONE
                    }
                })
        } else {
            // The ViewPropertyAnimator APIs are not available, so simply show
            // and hide the relevant UI components.
            login_progress.visibility = if (show) View.VISIBLE else View.GONE
            login_form.visibility = if (show) View.GONE else View.VISIBLE
        }
    }

    override fun onCreateLoader(i: Int, bundle: Bundle?): Loader<Cursor> {
        return CursorLoader(
            this,
            // Retrieve data rows for the device user's 'profile' contact.
            Uri.withAppendedPath(
                ContactsContract.Profile.CONTENT_URI,
                ContactsContract.Contacts.Data.CONTENT_DIRECTORY
            ), ProfileQuery.PROJECTION,

            // Select only email addresses.
            ContactsContract.Contacts.Data.MIMETYPE + " = ?", arrayOf(
                ContactsContract.CommonDataKinds.Email
                    .CONTENT_ITEM_TYPE
            ),

            // Show primary email addresses first. Note that there won't be
            // a primary email address if the user hasn't specified one.
            ContactsContract.Contacts.Data.IS_PRIMARY + " DESC"
        )
    }

    override fun onLoadFinished(cursorLoader: Loader<Cursor>, cursor: Cursor) {
        val emails = ArrayList<String>()
        cursor.moveToFirst()
        while (!cursor.isAfterLast) {
            emails.add(cursor.getString(ProfileQuery.ADDRESS))
            cursor.moveToNext()
        }

    }

    override fun onLoaderReset(cursorLoader: Loader<Cursor>) {
    }


    object ProfileQuery {
        val PROJECTION = arrayOf(
            ContactsContract.CommonDataKinds.Email.ADDRESS,
            ContactsContract.CommonDataKinds.Email.IS_PRIMARY
        )
        val ADDRESS = 0
    }

    class DatabaseManager {

        fun saveCredentials(user: LoginManager.User, curAct: LoginActivity) {
            var database = MyDatabaseOpenHelper(curAct)
            database.use {
                delete("User")
                insert("User",
                    "id" to user.id,
                    "username" to user.username,
                    "password" to user.password,
                    "accessToken" to user.accessToken,
                    "firstLogin" to user.isFirstLogin)
            }
        }

        fun retrieveCredentials(curAct: LoginActivity): LoginManager.User {
            var cred: LoginManager.User? = null

            var database = MyDatabaseOpenHelper(curAct)
            val username = database.use {
                select("User").limit(1)
            }
            //TODO
            //Figure out parsing data from query
            var ps = username.par

            return cred!!
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
                val reste = db.createTable("User", true,
                    "id" to INTEGER,
                    "username" to TEXT,
                    "password" to TEXT,
                    "accessToken" to TEXT,
                    "isFirstLogin" to TEXT
                )
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
}
