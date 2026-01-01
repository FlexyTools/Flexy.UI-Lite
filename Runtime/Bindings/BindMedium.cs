namespace Flexy.UI.Bindings
{
	public class BindMedium : BindableBehaviour, IAutoSetup
	{
		[SerializeField]	String	_expectedType = null!;

		[Bindable(TypeProvider=nameof(_expectedType))]
		public				Object?	DataObj {get;set;}

		public		void	Setup	( Object? data ) => DataObj = data;
	}
}