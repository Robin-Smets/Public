using NovelistBlazor.Common.DTO;

public class RepositoryEventMediator
{
    public event EventHandler<NovelDTO> CurrentNovelChanged;

    public void OnCurrentNovelChanged(NovelDTO novel)
    {
        CurrentNovelChanged?.Invoke(this, novel);
    }

    public event EventHandler<List<NovelDTO>> AvaiableNovelsChanged;

    public void OnAvaiableNovelsChanged(List<NovelDTO> novels)
    {
        AvaiableNovelsChanged?.Invoke(this, novels);
    }

    public event EventHandler<CharacterDTO> CurrentCharacterChanged;

    public void OnCurrentCharacterChanged(CharacterDTO character)
    {
        CurrentCharacterChanged?.Invoke(this, character);
    }

    public event EventHandler<List<CharacterDTO>> AllCurrentCharactersChanged;

    public void OnAllCurrentCharactersChanged(List<CharacterDTO> characters)
    {
        AllCurrentCharactersChanged?.Invoke(this, characters);
    }

    public event EventHandler<PlotUnitDTO> CurrentPlotUnitChanged;

    public void OnCurrentPlotUnitChanged(PlotUnitDTO plotUnit)
    {
        CurrentPlotUnitChanged?.Invoke(this, plotUnit);
    }

    public event EventHandler<List<PlotUnitDTO>> AllCurrentPlotUnitsChanged;

    public void OnAllCurrentPlotUnitsChanged(List<PlotUnitDTO> plotUnits)
    {
        AllCurrentPlotUnitsChanged?.Invoke(this, plotUnits);
    }
}
