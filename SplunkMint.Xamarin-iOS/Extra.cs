﻿using System;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;

namespace SplunkMint
{
	#region NSException extension class

	/// <summary>
	/// An extensions static class.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Helper System.Exception extension method.
		/// </summary>
		/// <returns>The Splunk>MINT NSException.</returns>
		/// <param name="exception">The System.Exception.</param>
		public static NSException ToSplunkNSException(this Exception exception)
		{
			NSMutableDictionary dictionary = new NSMutableDictionary ();
			dictionary.SetValueForKey (
				NSObject.FromObject(NSString.FromData(NSData.FromString(exception.StackTrace.Trim()), NSStringEncoding.UTF8)), 
				NSString.FromData(NSData.FromString("SplunkMint-Xamarin-Exception-Stacktrace"), NSStringEncoding.UTF8));

			string[] messages = exception.Message.Split (new [] { '\n' }, 1, StringSplitOptions.RemoveEmptyEntries);
			string message = null;
			if (messages != null &&
			    messages [0] != null) 
			{
				message = messages [0];
			}

			return new NSException (exception.GetType ().FullName, message != null ? message : exception.Message, dictionary);
		}
	}

	#endregion
}