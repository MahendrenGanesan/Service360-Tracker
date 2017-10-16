using System;

namespace GioPo.ViewModel
{
	public class MainViewModel
    {
        #region Fields

        private static MainViewModel instance = null;

        #endregion

        #region Constructor
        private MainViewModel()
		{
		}

        #endregion

        #region Properties

        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainViewModel();
                }
                return instance;
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}

