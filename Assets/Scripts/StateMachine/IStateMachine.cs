using System;
using System.Collections;

public interface IStateMachine : IDisposable
{
    public void Interract(ICharacter character);
    public ArrayList Save();
    public void Load(ArrayList data);
}
