﻿using AutoMapper;
using GalaSoft.MvvmLight.Command;
using MediatR;
using Microcharts;
using MoneyFox.Application.Categories.Queries.GetCategoryById;
using MoneyFox.Application.Common;
using MoneyFox.Application.Common.Messages;
using MoneyFox.Application.Statistics.Queries;
using MoneyFox.Extensions;
using MoneyFox.ViewModels.Categories;
using SkiaSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoneyFox.ViewModels.Statistics
{
    /// <summary>
    ///     Representation of the cash flow view.
    /// </summary>
    public class StatisticCategoryProgressionViewModel : StatisticViewModel
    {
        private readonly IMapper mapper;
        private BarChart chart = new BarChart();
        private bool hasNoData = true;
        private CategoryViewModel? selectedCategory;

        public StatisticCategoryProgressionViewModel(IMediator mediator,
            IMapper mapper) : base(mediator)
        {
            this.mapper = mapper;

            StartDate = DateTime.Now.AddYears(-1);

            MessengerInstance.Register<CategorySelectedMessage>(
                this,
                async message => await ReceiveMessageAsync(message));
        }

        public CategoryViewModel? SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if(selectedCategory == value)
                {
                    return;
                }

                selectedCategory = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Chart to render.
        /// </summary>
        public BarChart Chart
        {
            get => chart;
            set
            {
                if(chart == value)
                {
                    return;
                }

                chart = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Chart to render.
        /// </summary>
        public bool HasNoData
        {
            get => hasNoData;
            set
            {
                if(hasNoData == value)
                {
                    return;
                }

                hasNoData = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand LoadDataCommand => new RelayCommand(async () => await LoadAsync());

        public RelayCommand GoToSelectCategoryDialogCommand => new RelayCommand(
            async () => await Shell.Current.GoToModalAsync(ViewModelLocator.SelectCategoryRoute));

        public RelayCommand ResetCategoryCommand => new RelayCommand(() => SelectedCategory = null);


        private async Task ReceiveMessageAsync(CategorySelectedMessage message)
        {
            if(message == null)
            {
                return;
            }

            SelectedCategory =
                mapper.Map<CategoryViewModel>(await Mediator.Send(new GetCategoryByIdQuery(message.CategoryId)));
            await LoadAsync();
        }


        protected override async Task LoadAsync()
        {
            if(SelectedCategory == null)
            {
                HasNoData = true;
                return;
            }

            var statisticItems = await Mediator.Send(
                new GetCategoryProgressionQuery(SelectedCategory?.Id ?? 0, StartDate, EndDate));

            HasNoData = !statisticItems.Any();

            Chart = new BarChart
            {
                Entries =
                    statisticItems.Select(
                                      x => new ChartEntry((float)x.Value)
                                      {
                                          Label = x.Label,
                                          ValueLabel = x.ValueLabel,
                                          Color = SKColor.Parse(x.Color),
                                          ValueLabelColor = SKColor.Parse(x.Color)
                                      })
                                  .ToList(),
                BackgroundColor = new SKColor(ChartOptions.BackgroundColor.ToUInt()),
                Margin = ChartOptions.Margin,
                LabelTextSize = ChartOptions.LabelTextSize,
                Typeface = SKTypeface.FromFamilyName(ChartOptions.TypeFace)
            };
        }
    }
}