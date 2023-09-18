using System.Collections.ObjectModel;
using graphDLL;
using UCSearch;
using FrameSel;
using UCtypecheck;
using UCTogglebtn;
using graphglobal;
using static graphglobal.global;
using Accessibility;
using graphInterface;

namespace debug_test
{
    class MainViewModel : MVbase.MVBase
    {
        //private Model.MainModel model = null;
        
        public ObservableCollection<IUCSearch> m_ucsarch { get; set; }
        public ObservableCollection<UCFrameSel> m_listUCFrame { get; set; }
        
        public ObservableCollection<UCGraph> m_listUCGraph { get; set; }
        
        public ObservableCollection<UCtypecheckbox> m_listcheck { get; set; }

        public ObservableCollection<UCTogglebtn.UCTogglebtn> m_listToggle { get; set; }

        public bool RealTime { get => m_RealTime; set => m_RealTime = value; }

        private int m_curChart = 1;
        private bool m_RealTime = false;
        public Overrayview.OverView m_overView;
        public Overrayview.OverView OverView { get => m_overView; set => m_overView = value; }



        public MainViewModel()
        {
            m_ucsarch = new ObservableCollection<IUCSearch>();
            m_listUCGraph = new ObservableCollection<UCGraph>();
            m_listUCFrame = new ObservableCollection<UCFrameSel>();
            m_listcheck = new ObservableCollection<UCtypecheckbox>();
            m_listToggle = new ObservableCollection<UCTogglebtn.UCTogglebtn>();
            xline.Xline xl = new xline.Xline();

            IUCSearch search = new IUCSearch();
            m_ucsarch.Add(search);
            UCFrameSel uCFrame = new UCFrameSel();
            uCFrame.SetMargin(1, 1);
            m_listUCFrame.Add(uCFrame);
            
            UCGraph uCGraph = new UCGraph();
            uCGraph.SetMargin(1, 1);
            m_listUCGraph.Add(uCGraph);
            UCtypecheckbox uCtypecheckbox = new UCtypecheckbox();            
            uCtypecheckbox.SetMargin(1, 1);
            m_listcheck.Add(uCtypecheckbox);
            UCTogglebtn.UCTogglebtn uctoggle = new UCTogglebtn.UCTogglebtn();
            uctoggle.SetMargin(1, 1);
            m_listToggle.Add(uctoggle);
            
            search.interactor.Add(uCFrame);
            uCFrame.grpahinteractor = uCGraph;
            uCtypecheckbox.graphinter = uCGraph;
            uCFrame.checkinteractor = uCtypecheckbox;
            uCFrame.Toggleinter = uctoggle;
            uctoggle.Triter = uCGraph;
            uCGraph.m_interactor = uCtypecheckbox;
            //
            m_overView = new Overrayview.OverView();
        }

        public void CountChangeEvent(int _icount)
        {
            int a = _icount;
            if (m_curChart < _icount)
            {
                //add
                for (int i = m_curChart; i < _icount; ++i)
                {
                    UCFrameSel tempframe = new UCFrameSel();
                    UCGraph tempgraph = new UCGraph();
                    
                    UCtypecheckbox tempcheck = new UCtypecheckbox();
                    UCTogglebtn.UCTogglebtn tempuctoggle = new UCTogglebtn.UCTogglebtn();
                  //  postdb.DB pdb = new postdb.DB();
                    m_listUCGraph.Add(tempgraph);
                    m_listUCFrame.Add(tempframe);
                    m_listcheck.Add(tempcheck);
                    m_listToggle.Add(tempuctoggle);
                    //tempgraph.Vm

                    m_ucsarch[0].interactor.Add(tempframe);
                    tempframe.grpahinteractor = tempgraph;
                    tempcheck.graphinter = tempgraph;
                    tempframe.checkinteractor = tempcheck;
                    tempframe.Toggleinter = tempuctoggle;
                    tempuctoggle.Triter = tempgraph;
                    tempgraph.m_interactor = tempcheck;
                    // uCGraph.m_interactor = uCtypecheckbox;

                }
            }
            
            else if(m_curChart > _icount)
            { 
                //delete
                for(int i=m_curChart-1; i> _icount-1; --i)
                {
                    m_ucsarch[0].interactor.RemoveAt(i);
                    m_listUCFrame.RemoveAt(i);
                   m_listUCGraph.RemoveAt(i);
                   m_listcheck.RemoveAt(i);
                    m_listToggle.RemoveAt(i);
                }
            }


            for (int i = 0; i < _icount; ++i)
            {
                m_listUCFrame[i].SetMargin(_icount, i+1);
                m_listUCGraph[i].SetMargin(_icount, i + 1);
                m_listcheck[i].SetMargin(_icount, i+1);
                m_listToggle[i].SetMargin(_icount, i + 1);
            }

            m_curChart = _icount;
        }

    }



}
