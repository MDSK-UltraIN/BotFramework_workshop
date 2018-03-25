using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace 專案名稱.Dialogs
{
    [LuisModel("AppID", "Keys")]
    [Serializable]
    public class SimpleLuisDialog:LuisDialog<object>
    {

        [LuisIntent("None")]
        public async Task None(IDialogContext context ,LuisResult result)
        {
            string message = $"抱歉我涉世未深> < 您輸入的{result.Query}我還看不太懂";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }
        [LuisIntent("詢問吃喝")]
        public async Task Life(IDialogContext context,LuisResult result)
        {
            
            if(result.TopScoringIntent == null)
            {
                await None(context,result);
                return;
            }

            EntityRecommendation accountEntity = null;
            if(result.TryFindEntity("問句",out accountEntity))
            {
                if (result.TryFindEntity("時段::早上", out accountEntity))
                {
                    await context.PostAsync($"早上 吃手手");
                }
                else if (result.TryFindEntity("時段::中午", out accountEntity))
                {
                    await context.PostAsync($"中午 吃別人的手手");
                }
                else if(result.TryFindEntity("時段::晚上",out accountEntity))
                {
                    await context.PostAsync($"晚上 吃滿漢大餐");
                }
            }
            
            context.Wait(this.MessageReceived);
        }
    }
}