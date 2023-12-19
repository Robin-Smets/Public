using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelistBlazor.Common.Service
{
    public class PageAnalyzer
    {
        private NavigationManager _navigationManager;

        public PageAnalyzer(NavigationManager navigationManager) 
        { 
            _navigationManager = navigationManager;
        }

        public RoutedPage GetRoutedPage()
        {
            if (_navigationManager.Uri == _navigationManager.BaseUri)
            {
                return RoutedPage.Home;
            }
            else if (_navigationManager.Uri.Contains("/novel", StringComparison.OrdinalIgnoreCase))
            {
                return RoutedPage.Novel;
            }
            else if (_navigationManager.Uri.Contains("/character", StringComparison.OrdinalIgnoreCase))
            {
                return RoutedPage.Character;
            }
            else if (_navigationManager.Uri.Contains("/plot", StringComparison.OrdinalIgnoreCase))
            {
                return RoutedPage.Plot;
            }
            else if (_navigationManager.Uri.Contains("/text", StringComparison.OrdinalIgnoreCase))
            {
                return RoutedPage.Text;
            }
            else
            {
                return RoutedPage.Unexpected;
            }
        }
    }
}
