namespace Flexy.UI.Bindings
{
	public class BindMedium : BindableBehaviour, IAutoSetup
	{
		[SerializeField]	String	_expectedType = null!;

		[Bindable(TypeProvider=nameof(_expectedType))]
		public				object?	DataObj {get;set;}

		public		void	Setup	( object? data ) => DataObj = data;
	}
}