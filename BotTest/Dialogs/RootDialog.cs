using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using BotTest.Forms;
using Microsoft.Bot.Builder.FormFlow;
using System.Collections.Generic;
using System.Linq;

namespace BotTest.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            //De un dataSource de datos el Bot puede identificar al usuario del canal
            List<Contacto> listaContactos = new List<Contacto>();

            listaContactos.Add(new Contacto { Identificador = "default-user", Nombre = "Amin", Apellido = "Espinoza", Correo = "amines@microsoft.com", Telefono = "5555555555" });
            listaContactos.Add(new Contacto { Identificador = "11234523", Nombre = "Alejandro", Apellido = "Martinez", Correo = "alex@microsoft.com", Telefono = "1231231230" });
            listaContactos.Add(new Contacto { Identificador = "12ef32s2", Nombre = "Gabriel", Apellido = "Rodriguez", Correo = "gabriel@gmail.com", Telefono = "3453453459" });

            var activity = await result as Activity;
            //Cada canal tiene un ID - Skype Facebook etc
            string id = activity.From.Id;

            Contacto contactoElegido = listaContactos.FirstOrDefault(x => x.Identificador == id);
            await context.PostAsync(string.Format("Hola {0}", contactoElegido.Nombre));

            //Default
            // calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            //await context.PostAsync($"Enviaste {activity.Text} que tiene {length} caracteres");

            //Harcoding conversation
            //string mensajeRecibido = activity.Text.ToLower();
            //switch (mensajeRecibido)
            //{
            //    case "hola":
            //        await context.PostAsync($"Que onda carnal?");
            //        break;
            //    case "hola, soy americanista":
            //        await context.PostAsync($"Entonces no me hables");
            //        break;
            //    case "quiero crear mi perfil":
            //        await Conversation.SendAsync(activity, CrearFormulario); //No funca a esperar la prox clase - Se debe lanzar un Dialogo desde aca
            //        break;
            //    default:
            //        await context.PostAsync($"Lo siento, esta no me la se");
            //        break;
            //}

            context.Wait(MessageReceivedAsync);
        }

        private static IDialog<ContactForm> CrearFormulario()
        {
            return Chain.From(() => FormDialog.FromForm(ContactForm.BuildForm));
        }

    }
}