using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CardGridLayout : LayoutGroup
{
    public int rows = 4;                 // Number of rows in the grid layout
    public int columns = 5;              // Number of columns in the grid layout
    public Vector2 spacing = new Vector2(10f, 10f);   // Spacing between cards in the grid layout
    public int preferredPadding = 0; // Preferred top padding for the grid layout

    // Override method to calculate the layout input vertically
    public override void CalculateLayoutInputVertical()
    {
        // Ensure rows and columns are not zero, set default values if they are
        if (rows == 0 || columns == 0)
        {
            rows = 5;
            columns = 6;
        }

        // Get the width and height of the parent RectTransform
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        // Calculate the width and height of each card based on the number of rows and columns
        float cardHeight = (parentHeight - spacing.y * (rows - 1)) / rows;
        float cardWidth = cardHeight;

        // Adjust card width if necessary to fit within parent width
        if (cardWidth * columns + spacing.x * (columns - 1) > parentWidth)
        {
            cardWidth = (parentWidth - 2 * preferredPadding - (columns - 1) * spacing.x) / columns;
            cardHeight = cardWidth;
        }

        // Calculate padding for left, top, and bottom sides of the layout
        Vector2 cardSize = new Vector2(cardWidth, cardHeight);
        padding.left = Mathf.FloorToInt((parentWidth - columns * cardWidth - spacing.x * (columns - 1)) / 2);
        padding.top = Mathf.FloorToInt((parentHeight - rows * cardHeight - spacing.y * (rows - 1)) / 2);
        padding.bottom = padding.top;

        // Position each child element (card) within the layout
        for (int i = 0; i < rectChildren.Count; i++)
        {
            int rowCount = i / columns;
            int columnCount = i % columns;
            var item = rectChildren[i];

            var xPos = padding.left + cardSize.x * columnCount + spacing.x * columnCount;
            var yPos = padding.top + cardSize.y * rowCount + spacing.y * rowCount;

            SetChildAlongAxis(item, 0, xPos, cardSize.x);
            SetChildAlongAxis(item, 1, yPos, cardSize.y);
        }
    }

    // Override method to set layout horizontally
    public override void SetLayoutHorizontal()
    {
        return;
    }

    // Override method to set layout vertically
    public override void SetLayoutVertical()
    {
        return;
    }
}
