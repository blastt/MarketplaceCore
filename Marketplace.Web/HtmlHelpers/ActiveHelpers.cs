using Hangfire.Dashboard;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.HtmlHelpers
{
    public static class ActiveHelpers
    {
        public static string IsSelected(this IHtmlHelper html, string controllers = "", string actions = "", string cssClass = "active")
        {

            var routeValues = html.ViewContext.HttpContext.GetRouteData().Values;

            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(actions))
                actions = currentAction;

            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedActions = actions.ToLower().Trim().Split(',').Distinct().ToArray();
            string[] acceptedControllers = controllers.ToLower().Trim().Split(',').Distinct().ToArray();

            return acceptedActions.Contains(currentAction.ToLower()) && acceptedControllers.Contains(currentController.ToLower()) ?
                cssClass : String.Empty;
        }

        public static string IsSelectedGame(this IHtmlHelper html, string controllers = "", string actions = "", string games = "", string cssClass = "active")
        {
            var routeValues = html.ViewContext.HttpContext.GetRouteData().Values;
            string currentGame = routeValues["game"] == null ? "csgo" : routeValues["game"].ToString();
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(games))
                games = currentAction;

            if (String.IsNullOrEmpty(actions))
                actions = currentAction;

            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedGames = games.ToLower().Trim().Split(',').Distinct().ToArray();
            string[] acceptedActions = actions.ToLower().Trim().Split(',').Distinct().ToArray();
            string[] acceptedControllers = controllers.ToLower().Trim().Split(',').Distinct().ToArray();
            string str = acceptedActions.Contains(currentAction.ToLower()) && acceptedGames.Contains(currentGame.ToLower()) && acceptedControllers.Contains(currentController.ToLower()) ?
                cssClass : String.Empty;
            return str;
        }
    }
}
