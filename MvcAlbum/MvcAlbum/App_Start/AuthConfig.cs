﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using MvcAlbum.Models;

namespace MvcAlbum
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // このサイトのユーザーが、Microsoft、Facebook、および Twitter などの他のサイトのアカウントを使用してログインできるようにするには、
            // このサイトを更新する必要があります。詳細については、http://go.microsoft.com/fwlink/?LinkID=252166 を参照してください

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
