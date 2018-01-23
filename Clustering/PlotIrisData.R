library(ggplot2, quietly = TRUE)
library(GGally, quietly = TRUE)
data("iris")

# length vs. width
plot <- ggplot2::ggplot()
plot <- plot + ggplot2::ggtitle(label = "Iris Data")
plot <- plot + ggplot2::xlab("Length")
plot <- plot + ggplot2::ylab("Width")
plot <- plot + ggplot2::labs(colour = "Feature")
plot <- plot + ggplot2::geom_point(data = iris, mapping = ggplot2::aes(x = Petal.Length, y = Petal.Width, colour = Species, shape = "petal"))
plot <- plot + ggplot2::geom_point(data = iris, mapping = ggplot2::aes(x = Sepal.Length, y = Sepal.Width, colour = Species, shape = "sepal"))
print(plot)
GGally::ggsave("iris data.pdf", plot = plot, device = "pdf", scale = 1, dpi = 300, width = 100, height = 100, units = "mm")

# matrix plot
plot <- GGally::ggpairs(data = iris, mapping = ggplot2::aes(colour = Species))
print(plot)
ggplot2::ggsave("iris data pairs.png", plot = plot, device = "png", scale = 1, dpi = 300, width = 100, height = 100, units = "mm")
