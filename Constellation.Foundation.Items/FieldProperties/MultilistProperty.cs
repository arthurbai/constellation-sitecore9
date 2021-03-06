﻿namespace Constellation.Foundation.Items.FieldProperties
{
	using Constellation.Foundation.Items;
	using Sitecore.Data;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

	/// <inheritdoc />
	/// <summary>
	/// Facade for a Sitecore Multilist Field.
	/// </summary>
	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Name of Sitecore field.")]
	public class MultilistProperty : DelimitedProperty
	{
		/// <summary>
		/// The field to wrap.
		/// </summary>
		private readonly MultilistField _multilistField;

		#region Constructors
		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the <see cref="MultilistProperty"/> class.
		/// </summary>
		/// <param name="field">The field to wrap.</param>
		public MultilistProperty(Field field)
			: base(field)
		{
			_multilistField = field;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the list of target IDs.
		/// </summary>
		public ID[] TargetIDs => _multilistField.TargetIDs;

		#endregion

		#region Operators
		/// <summary>
		/// Allows interoperability with Sitecore MultilistField.
		/// </summary>
		/// <param name="field">The field to wrap.</param>
		/// <returns>A new instance of MultilistProperty using the supplied field.</returns>
		public static implicit operator MultilistProperty(MultilistField field)
		{
			return new MultilistProperty(field.InnerField);
		}

		/// <summary>
		/// Allows interoperability with Sitecore MultilistField.
		/// </summary>
		/// <param name="property">The property to convert.</param>
		/// <returns>The property.InnerField.</returns>
		public static implicit operator MultilistField(MultilistProperty property)
		{
			return property.InnerField;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <returns>
		/// The items.
		/// </returns>
		public Item[] GetItems()
		{
			return _multilistField.GetItems();
		}

		/// <summary>
		/// Gets a collection of <see cref="IStandardTemplate"/> items.
		/// </summary>
		/// <returns>A collection of IStandardTemplateItem, the collection may be empty.</returns>
		public virtual ICollection<IStandardTemplate> GetTargetItems()
		{
			var list = new List<IStandardTemplate>();

			if (HasValue)
			{
				foreach (var id in TargetIDs)
				{
					var item = Item.Database.GetItem(id);

					if (item != null)
					{
						list.Add(item.AsStronglyTyped());
					}
				}
			}

			return list;
		}

		/// <summary>
		/// Gets a collection TItem based on the field's target items. Only Items that are of the
		/// supplied Type are returned.
		/// </summary>
		/// <typeparam name="TItem">The Item type to filter for.</typeparam>
		/// <returns>A collection of TItem. The collection  may be empty.</returns>
		public virtual ICollection<TItem> GetTargetItems<TItem>() where TItem : class, IStandardTemplate
		{
			var list = new List<TItem>();

			if (HasValue)
			{
				var items = GetTargetItems();

				list.AddRange(items.OfType<TItem>().Select(item => item));
			}

			return list;
		}
		#endregion
	}
}
