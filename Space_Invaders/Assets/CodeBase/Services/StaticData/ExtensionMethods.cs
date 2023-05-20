using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeBase.Services.StaticData
{
	public static class ExtensionMethods
	{
		public static T DeepCopy<T>(this T self)
		{
			using (var stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, self);
				stream.Seek(0, SeekOrigin.Begin);
				object copy = formatter.Deserialize(stream);
				return (T) copy;
			}
		}
	}
}