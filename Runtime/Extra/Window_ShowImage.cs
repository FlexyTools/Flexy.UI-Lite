namespace Flexy.UI.Extra
{
	public class Window_ShowImage : State
	{
		[SerializeField] Single		_showTime	= 2;

		protected override	UniTask	OnShow				( )		
		{
			ShowAllImagesAsync().Forget();
			return default;
		}
		private async		UniTask	ShowAllImagesAsync	( )		
		{
			await UniTask.Delay( TimeSpan.FromSeconds(_showTime) );
				
			Close();
		}
	}
}