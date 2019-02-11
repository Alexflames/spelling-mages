using UnityEngine;

public interface SpellInit
{
	void cast(string smName);
    void RMBReact();
    string Description {get; }
	string[] Aliases {get; }
}
