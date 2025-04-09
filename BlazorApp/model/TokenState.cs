namespace BlazorApp.Model{

    public class TokenState
    {
        public string? Token { get; private set; }
        public string Languaje {get; private set;} = "kjv";
        public event Action? OnChange;
        private readonly SynchronizationContext? _syncContext = SynchronizationContext.Current;

        public void SetToken(string TokenIn)
        {
            Token = TokenIn;
            NotifyStateChanged();
        }

        public void SetLanguage(string lan){
            Languaje = lan;
        }

        private void NotifyStateChanged()
        {
            if (_syncContext != null)
            {
                _syncContext.Post(_ => OnChange?.Invoke(), null);
            }
            else
            {
                OnChange?.Invoke();
            }
        }
    }

}