namespace Flexy.UI.Bindings
{
	#if UNITY_EDITOR
	public class BindTestPolygon : MonoBehaviour
	{
		[Bindable]	public String	Str		{ get;set; }
		[Bindable]	public Int32	Int32	{ get;set; }
		[Bindable]	public Single	Single	{ get;set; }
		[Bindable]	public Boolean	Boolean	{ get;set; }
		
		[Bindable]	public BindTestEnum	ExactEnum	{ get;set; }
		
		[Bindable]	public Boolean BarE	( BindTestEnum index )
		{
			Debug.Log		( "[BindTestPolygon] - Foo: " + index );
			return false;
		}
		
		[Callable]	public void FooI	( Int32 index )
		{
			Debug.Log		( "[BindTestPolygon] - Foo: " + index );
		}
		[Callable]	public void FooB	( Boolean index )
		{
			Debug.Log		( "[BindTestPolygon] - Foo: " + index );
		}
		[Callable]	public void FooS	( Single index )
		{
			Debug.Log		( "[BindTestPolygon] - Foo: " + index );
		}
		[Callable]	public void FooE	( BindTestEnum index )
		{
			Debug.Log		( "[BindTestPolygon] - Foo: " + index );
		}
		[Callable]	public void FooO	( GameObject obj )
		{
			Debug.Log		( "[BindTestPolygon] - Foo: " + obj.name );
		}
	}
	
	public enum BindTestEnum : Byte
	{
		Value1,
		Value2,
		Value3,
	}
	#endif
}