namespace LibraryHelper
{
	using System;
	using System.Collections.Generic;
	using Caliburn.Micro;
    using LibraryHelper.ViewModels;

    /// <summary>
    /// Personal bootstrapper inherited from Caliburn.Micro BootstrapperBase
    /// </summary>
	public class AppBootstrapper : BootstrapperBase
	{
        /// <summary>
        /// Caliburn.Micro IoC Container
        /// </summary>
		SimpleContainer container;

		public AppBootstrapper()
		{
            // BootstrapperBase and PhoneBootstrapperBase do not call the Start() method in the constructor,
            // so it's needed!
            Start();
		}

		protected override void Configure()
		{
			container = new SimpleContainer();

			container.Singleton<IWindowManager, WindowManager>();
			container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<IShell, ShellViewModel>();
            container.Singleton<MainViewModel>();

            AddCustomConventions();
		}

        /// <summary>
        /// Add custom Conventions to ConventionManager.
        /// <seealso cref="https://caliburnmicro.codeplex.com/wikipage?title=All%20About%20Conventions" />
        /// </summary>
        private void AddCustomConventions()
        {
            // TODO ... here ConventionManager!
        }

		protected override object GetInstance(Type service, string key)
		{
			var instance = container.GetInstance(service, key);
			if (instance != null)
				return instance;

			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.BuildUp(instance);
		}

		protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
		{
            // Entry-Point
			DisplayRootViewFor<MainViewModel>();
		}
	}
}