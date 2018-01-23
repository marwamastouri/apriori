############################################
# Expectation-Maximization (EM) Clustering #
############################################
library(ggplot2, quietly = TRUE)
library(ggalt, quietly = TRUE)
library("EMCluster", quietly = TRUE)
require(plyr, quietly = TRUE)

data("iris")
set.seed(12345)

irisData <- iris[, 1:4]
number_of_clusters <- 6
config <- EMCluster::simple.init(irisData, nclass = number_of_clusters)
result <- EMCluster::emcluster(irisData, config, assign.class = TRUE)
iris$Cluster <- paste("Cluster", factor(result$class))

# setup plot
plot <- ggplot2::ggplot()
plot <- plot + ggplot2::ggtitle(label = "Iris Data")
plot <- plot + ggplot2::xlab("Length")
plot <- plot + ggplot2::ylab("Width")
plot <- plot + ggplot2::labs(colour = "Feature")
# plot iris data
plot <- plot + ggplot2::geom_point(data = iris, mapping = ggplot2::aes(x = Petal.Length, y = Petal.Width, colour = Species, shape = "petal"))
plot <- plot + ggplot2::geom_point(data = iris, mapping = ggplot2::aes(x = Sepal.Length, y = Sepal.Width, colour = Species, shape = "sepal"))
# enclose clusters
plot <- plot + ggalt::geom_encircle(data = iris, mapping = ggplot2::aes(x = Petal.Length, y = Petal.Width, colour = Cluster))
plot <- plot + ggalt::geom_encircle(data = iris, mapping = ggplot2::aes(x = Sepal.Length, y = Sepal.Width, colour = Cluster))
# TODO: print result
print(plot)
ggplot2::ggsave("iris data em.png", plot = plot, device = "png", scale = 1, dpi = 300, width = 100, height = 100, units = "mm")

# centers plot
centersData <- reshape2::melt(result$Mu)
names(centersData) <- c("Cluster", "Measurement", "Centimeters")
centersData$Measurement <- plyr::mapvalues(centersData$Measurement, from = c(1, 2, 3, 4), to = c("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width"))
centersData$Cluster <- factor(centersData$Cluster)

plot <- ggplot2::ggplot(data = centersData, ggplot2::aes(x = Measurement, y = Centimeters, group = Cluster)) +
       ggplot2::geom_point(size = 3, ggplot2::aes(shape = Cluster, color = Cluster)) +
       ggplot2::geom_line(size = 1, ggplot2::aes(color = Cluster)) +
       ggplot2::ggtitle("Profiles for Iris Clusters")
print(plot)
