namespace Flexy.UI.Extra
{
	public class Window_ShowImage : State
	{
		[SerializeField] Single		_showTime	= 2;

		protected override	UniTask	OnShow			( )		
		{
			DelayedClose().Forget();
			return default;
		}
		private async		UniTask	DelayedClose	( )		
		{
			await UniTask.Delay( TimeSpan.FromSeconds(_showTime) );
				
			Close();
		}
	}
}