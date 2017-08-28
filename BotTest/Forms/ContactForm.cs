using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotTest.Forms
{
    [Serializable]
    public class ContactForm
    {
        [Prompt("Cual es tu nombre?")]
        public string Nombre { get; set; }

        [Prompt("Cual es tu apellido?")]
        public string Apellido { get; set; }

        [Prompt("Cual es tu Edad?")]
        public int Edad { get; set; }

        [Prompt("Eres hombre o  mujer??")]
        public Genero Genero { get; set; }

        public static IForm<ContactForm> BuildForm()
        {
            return new FormBuilder<ContactForm>()
            .Message("Hola, vamos a comenzar a crear tu perfil")
            .OnCompletion(async (context, ContactForm) =>
            {
                await context.PostAsync("Tu perfil esta completo.");
            })
            .Build();

        }
    }

    [Serializable]
    public enum Genero
    {
        Hombre = 1,
        Mujer = 2
    }
}