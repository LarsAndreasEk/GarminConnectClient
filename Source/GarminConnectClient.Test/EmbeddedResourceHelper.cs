using System;
using System.IO;
using System.Reflection;

namespace GarminConnectClient.Test
{
	public class EmbeddedResourceHelper
	{
		private readonly Assembly assembly;

		public EmbeddedResourceHelper(Assembly assembly)
		{
			if (assembly == null) throw new ArgumentNullException("assembly");

			this.assembly = assembly;
		}

		/// <summary>
		/// Gets the resource as stream.
		/// </summary>
		/// <remarks>Throws <see cref="EmbeddedResourceNotFoundException"/> if the resource was not 
		/// found or the caller could not get access to it.</remarks>
		public Stream GetResourceAsStream(string resourceName)
		{
			var stream = assembly.GetManifestResourceStream(resourceName);
			if (stream == null) throw new EmbeddedResourceNotFoundException(resourceName);
			return stream;
		}

		/// <summary>
		/// Gets the resource as string.
		/// </summary>
		/// <remarks>Throws <see cref="EmbeddedResourceNotFoundException"/> if the resource was not 
		/// found or the caller could not get access to it.</remarks>
		public string GetResourceAsString(string resourceName)
		{
			using (var stream = GetResourceAsStream(resourceName))
			{
				return (new StreamReader(stream)).ReadToEnd();
			}
		}

		/// <summary>
		/// Gets the resource as byte array.
		/// </summary>
		/// <remarks>Throws <see cref="EmbeddedResourceNotFoundException"/> if the resource was not 
		/// found or the caller could not get access to it.</remarks>
		public byte[] GetResourceAsBytes(string resourceName)
		{
			using (var stream = GetResourceAsStream(resourceName))
			{
				return ReadAllBytesFromStream(stream);
			}
		}

		private static byte[] ReadAllBytesFromStream(Stream stream)
		{
			var memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			return memoryStream.ToArray();
		}
	}

	public class EmbeddedResourceNotFoundException : Exception
	{
		public EmbeddedResourceNotFoundException(string resourceName)
			: base(String.Format("Could not find or get access to resource '{0}'.", resourceName))
		{ }
	}
}
