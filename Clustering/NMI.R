#######################################
# normalized mutual information (NMI) #
#######################################

library("NMI", quietly = TRUE)
library("EMCluster", quietly = TRUE)
library(dbscan, quietly = TRUE)

data("iris")
set.seed(12345)

# K-Means
number_of_clusters <- 6
kmeans_result <- kmeans(iris[, 1:4], number_of_clusters, nstart = 60)

# DBScan
dbscan_result <- dbscan::dbscan(iris[, 1:4], eps = .4, minPts = 4)

irisData <- iris[, 1:4]
number_of_clusters <- 6
config <- EMCluster::simple.init(irisData, nclass = number_of_clusters)
em_result <- EMCluster::emcluster(irisData, config, assign.class = TRUE)

# calculate the correlation of the values from clustering algorithms


kmeans_result_f <- data.frame(seq(1, length(kmeans_result$cluster), 1), kmeans_result$cluster);
dbscan_result_f <- data.frame(seq(1, length(dbscan_result$cluster), 1), dbscan_result$cluster);
em_result_f <- data.frame(seq(1, length(em_result$class), 1), em_result$class);

nmi_kmeans_dbscan <- NMI(kmeans_result_f, dbscan_result_f)
nmi_kmeans_em <- NMI(kmeans_result_f, em_result_f)
nmi_dbscan_em <- NMI(dbscan_result_f, em_result_f)

result <- c(nmi_kmeans_dbscan$value, nmi_kmeans_em$value, nmi_dbscan_em$value)
print(result)
