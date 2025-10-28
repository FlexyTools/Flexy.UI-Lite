namespace Flexy.UI.Bindings
{
	public class BindMedium : BindableBehaviour, IAutoSetup
	{
		[SerializeField]	String	ExpectedType = null!;

		[Bindable(TypeProvider=nameof(ExpectedType))]
		public				Object?	DataObj {get;set;}

		public		void	Setup	( Object? data ) => DataObj = data;
	}
}