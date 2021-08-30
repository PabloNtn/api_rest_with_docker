using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiDocker.Model;

namespace ApiDocker.Business
{
    public interface IDirectorShipBusiness
    {
        DirectorShip FindByUserName(string username, string password);
        List<DirectorShip> FindAll();
        ActionResult<DirectorShip> Create(DirectorShip director);
        ActionResult<int> Delete(long id);
    }
}
