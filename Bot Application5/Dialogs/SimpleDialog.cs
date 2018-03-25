using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Bot_Application5.Dialogs //修改為自訂專案名
{
    [Serializable]
    public class SimpleDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hello 您好");
            context.Wait(MessageReciveAsync1);
        }

        private async Task MessageReciveAsync1(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            
            await context.PostAsync($"請問您的大名是?");
            context.Wait(MessageReciveAsync2);
        }

        private async Task MessageReciveAsync2(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            await context.PostAsync($"{activity.Text} 您好 很高興認識你 你怎麼知道我的?");
            context.Wait(MessageReciveAsync3);
        }

        private async Task MessageReciveAsync3(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            await context.PostAsync($"原來如此 看來你是個好人 我們緣分已盡");
            context.Wait(MessageReciveAsync1);
        }
    }
}