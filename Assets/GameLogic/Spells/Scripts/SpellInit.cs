using UnityEngine;

public interface SpellInit
{
	void cast(string smName);
	string Description {get; }
	string[] Aliases {get; }
}
