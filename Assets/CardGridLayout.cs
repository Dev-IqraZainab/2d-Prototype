
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CardGridLayout : LayoutGroup
{
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 5;
    [SerializeField] private Vector2 spacing = new Vector2(10f, 10f);
    [SerializeField] private int preferredTopPadding = 0;
  
    public override void CalculateLayoutInputVertical()
    {
        if (rows == 0 || columns == 0)
        {
            rows = 4;
            columns = 5;

        }
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cardHeight = (parentHeight - spacing.y * (rows - 1)) / rows;
        float cardWidth = cardHeight;

        if (cardWidth * columns + spacing.x * (columns - 1) > parentWidth)
        {
            cardWidth = (parentWidth - 2 * preferredTopPadding - (columns - 1) * spacing.x) / columns;
            cardHeight = cardWidth;
        }
        Vector2 cardSize = new Vector2(cardWidth, cardHeight);
        padding.left = Mathf.FloorToInt((parentWidth - columns * cardWidth- spacing.x*(columns-1)) / 2);
        padding.top = Mathf.FloorToInt((parentHeight - rows * cardHeight- spacing.y*(rows -1)) / 2);
        padding.bottom = padding.top;

        for(int i=0; i<rectChildren.Count; i++)
        {
            int rowCount = i / columns;
            int columnCount = i % columns;
            var item = rectChildren[i];

            var xPos = padding.left+ cardSize.x * columnCount + spacing.x *(columnCount);
            var yPos = padding.top+cardSize.y * rowCount+spacing.y* (rowCount);
        
            SetChildAlongAxis(item, 0, xPos, cardSize.x);
            SetChildAlongAxis(item, 1, yPos, cardSize.y);

        }
    }

    public override void SetLayoutHorizontal()
    {
        return;
    }

    public override void SetLayoutVertical()
    {
        return;
    }


}
