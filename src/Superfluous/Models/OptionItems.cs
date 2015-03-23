using System;
using Xamarin.Forms;

namespace Superfluous
{
	public class OptionItem
	{
		public virtual string Title { get; set; }
		public virtual int Count { get; set; }
		public virtual bool Selected { get; set; }
		public virtual string Icon { get { return 
				Title.ToLower().TrimEnd('s') + ".png" ; } }
		public ImageSource IconSource { get { return ImageSource.FromFile(Icon); } }
	}
}

