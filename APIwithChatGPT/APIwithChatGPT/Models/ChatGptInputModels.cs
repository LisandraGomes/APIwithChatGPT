﻿namespace APIChatGPT.Models
{
    public class ChatGptInputModels
    {
        //Facilita os parametros da requisição. definindo a forma de retorno.
        public ChatGptInputModels(string prompt)
        {
            //this.prompt = $"Correct this english phrase: {prompt}";
            temperature = 0.2m;
            max_tokens = 100;
            model = "text-davinci-003";

        }

        public string model { get; set; }
        public string prompt { get; set; }
        public int max_tokens { get; set; }
        public decimal temperature { get; set; }
    }
}
