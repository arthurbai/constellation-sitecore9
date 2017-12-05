﻿namespace Constellation.Feature.Navigation.Models
{
	/// <summary>
	/// Item information essential to rendering a link to that Item.
	/// </summary>
	public class TargetItem
	{
		/// <summary>
		/// The Item's Display Name
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// The Item's URL as created by the context LinkManager settings.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// The Item's Navigation Title field value, if the Item inherits from Page Navigation Title (recommended for pages).
		/// </summary>
		public string NavigationTitle { get; set; }
	}
}
