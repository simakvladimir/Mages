﻿namespace Mages.Repl
{
    using System;

    public interface IInteractivity
    {
        event AutoCompleteHandler AutoComplete;

        IDisposable HandleCancellation(Func<Boolean> shouldCancel);

        String ReadLine();

        String GetLine(String prompt);

        void Write(String output);

        void Info(String result);

        void Error(String message);
    }
}
