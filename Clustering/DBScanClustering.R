###################################################################################
# Density-based spatial clustering of applications with noise (DBSCAN) Clustering #
###################################################################################

# load packages
library(dbscan, quietly = TRUE)
library(ggplot2, quietly = TRUE)
library(ggalt, quietly = TRUE)
data("iris")
set.seed(12345)

result <- dbscan::dbscan(iris[, 1:4], eps = .4, minPts = 4)

# prepare data for plotting
iris$Cluster <- paste("Cluster", factor(result$cluster))

# setup plot
plot <- ggplot2::ggplot()
plot <- plot + ggplot2::ggtitle(label = "Iris Data (DBSCAN clusters)")
plot <- plot + ggplot2::xlab("Length")
plot <- plot + ggplot2::ylab("Width")
plot <- plot + ggplot2::labs(colour = "Feature")
# plot iris data
plot <- plot + ggplot2::geom_point(data = iris, mapping = ggplot2::aes(x = Petal.Length, y = Petal.Width, colour = Species, shape = "petal"))
plot <- plot + ggplot2::geom_point(data = iris, mapping = ggplot2::aes(x = Sepal.Length, y = Sepal.Width, colour = Species, shape = "sepal"))
# enclose clusters
plot <- plot + ggalt::geom_encircle(data = iris, mapping = ggplot2::aes(x = Petal.Length, y = Petal.Width, colour = Cluster))
plot <- plot + ggalt::geom_encircle(data = iris, mapping = ggplot2::aes(x = Sepal.Length, y = Sepal.Width, colour = Cluster))
print(plot)
ggplot2::ggsave("iris data dbscan.png", plot = plot, device = "png", scale = 1, dpi = 300, width = 100, height = 100, units = "mm")
