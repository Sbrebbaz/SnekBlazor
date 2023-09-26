using System.Drawing;
using static SnekBlazor.Classes.Enumerators;

namespace SnekBlazor.Pages
{
	public partial class Index
	{
		private Point PlayerPosition = new Point();

		public void DirectionChanged(MovementDirection direction)
		{
			switch (direction)
			{
				case MovementDirection.Up:
					{
						PlayerPosition.Y--;
						break;
					}
				case MovementDirection.Down:
					{
						PlayerPosition.Y++;
						break;
					}
				case MovementDirection.Left:
					{
						PlayerPosition.X--;
						break;
					}
				case MovementDirection.Right:
					{
						PlayerPosition.X++;
						break;
					}
			}
			StateHasChanged();
		}

	}
}
