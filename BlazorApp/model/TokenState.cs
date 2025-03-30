namespace BlazorApp.Model{

    public class TokenState
    {
        public string? Token { get; private set; }
        public event Action? OnChange;
        private readonly SynchronizationContext? _syncContext = SynchronizationContext.Current;

        public void SetToken(string TokenIn)
        {
            Token = TokenIn;
            NotifyStateChanged();
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