namespace ITLand.Web.Extensions
{
	public static class StringExtensions
	{
		public static string TruncateAtWord(this string input, int noWords)
		{
			var output = string.Empty;
			var inputArr = input.Split(' ');

			if (inputArr.Length <= noWords)
				return input;

			if (noWords > 0)
			{
				for (var i = 0; i < noWords; i++)
				{
					output += inputArr[i] + " ";
				}

				output += "...";
				return output;
			}

			return input;
		}
	}
}
