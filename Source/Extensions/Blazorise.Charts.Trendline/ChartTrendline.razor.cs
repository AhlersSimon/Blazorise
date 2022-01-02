﻿#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
#endregion

namespace Blazorise.Charts.Trendline
{
    public interface IChartTrendline
    {
        Task AddTrendLines( List<ChartTrendline> trendlines );
    }

    /// <summary>
    /// Provides the streaming capabilities to the supported chart types.
    /// </summary>
    /// <typeparam name="TItem">Data point type.</typeparam>
    public partial class ChartTrendline<TItem> : BaseComponent, IChartTrendline, IAsyncDisposable
    {
        protected override Task OnInitializedAsync()
        {
            if ( JSModule == null )
            {
                JSModule = new JSChartTrendlineModule( JSRuntime, VersionProvider );
            }

            return base.OnInitializedAsync();
        }

        public async Task AddTrendLines(List<ChartTrendline> trendlines)
        {
            if ( !Rendered )
                return;

            await JSModule.AddTrendlines( ParentChart.ElementId, trendlines );
        }

        /// <inheritdoc/>
        protected override bool ShouldAutoGenerateId => true;

        protected JSChartTrendlineModule JSModule { get; private set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        [Inject] private IVersionProvider VersionProvider { get; set; }

        [CascadingParameter] protected BaseChart<TItem> ParentChart { get; set; }
       
    }
}