namespace Flexy.UI.Extra
{
	public class Window_ShowImage : State
	{
		[SerializeField] Single		_showTime	= 2;

		protected override	void	OnShow				( )		
		{
			ShowAllImagesAsync().Forget();
		}
		private async		UniTask	ShowAllImagesAsync	( )		
		{
			await UniTask.Delay( TimeSpan.FromSeconds(_showTime) );
				
			Close();
		}
	}
}