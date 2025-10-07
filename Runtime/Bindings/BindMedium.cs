namespace Flexy.UI.Bindings
{
	public class BindMedium : BindableBehaviour, IAutoSetup
	{
		public		Object  DataStruct {get;set;}
	
		public		void	Setup	( Object data )	
		{
			DataStruct = data;
		}
	}
}