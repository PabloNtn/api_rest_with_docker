using ApiDocker.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiDocker.Repository
{
    public interface IDirectorShipRepository
    {
        DirectorShip FindByUserName(string username, string password);
        List<DirectorShip> FindAll();
        ActionResult<DirectorShip> Create(DirectorShip director);
        ActionResult<int> Delete(long id);
    }
}
